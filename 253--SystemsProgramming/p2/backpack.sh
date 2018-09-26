#!/bin/bash
if [ $# != 2 ];then
  echo "usage: $0 <checkoff> <output file>"
  echo "   checkoff - the checkoff you are testing"
  echo "   output file - the file to save the grades in"
  exit 0;
fi
chkoff="$1"
dest="$2"
exe="smash"

# Tests basic shell functionality
function test_checkoff_one()
{
	in="test.in"
	expected="test.out"
	actual="smash.out"

	./$exe < $in > $actual 2> /dev/null

	diff $expected $actual > $actual.diff
	if [ "$?" != 0 ];then
	     echo "P2: FAIL output incorrect. See $actual.diff for differences." >> $dest
	     exit 0
	else
		rm -f $actual.diff $actual
	fi
}

# Tests exit and cd shell functionality
function test_checkoff_two()
{
	# Feed 'exit' into smash and check return result
	./$exe <<- EOF 2> /dev/null
	exit
	EOF

	if [ "$?" != 0 ];then
	     echo "P2: Did not exit successfully" >> $dest
	     exit 0
	fi

	# Feed 'cd $HOME' into smash and store resulting output
	result=$(
	./$exe <<- EOF 2> /dev/null
	cd $HOME
	EOF
	)

	if [ "$result" != "$HOME" ]; then
	     echo "P2: Did not change directories correctly" >> $dest
	     exit 0
	fi
}

#Generate the students assignment
make

#Make sure the program built
if [ ! -e $exe ];then
  echo "P2: FAIL reason: Did not build the right executable" >> $dest
  exit 1 # fail the build
fi

test_checkoff_one
if [ $chkoff -eq 2 ]; then
	test_checkoff_two
fi

make clean
if [ "$?" != 0 ];then
        #We failed
        echo "P2: FAIL make clean" >> $dest
        exit 0
fi

make clean
echo "P2: PASS" >> $dest
